﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Engine;

namespace SpikeWpf.Conversation
{
	/// <summary>
	/// This is an implementation of Conversation per Business Transaction pattern.
	/// Doesn't work with tables with "assigned" generator class (like tables with IDENTITY column in SQL Server)
	/// See http://fabiomaulo.blogspot.com/2008/12/identity-never-ending-story.html
	/// </summary>
	public class Conversation : IConversation
	{
		private readonly IDictionary<ISessionFactory, ISession> _map;
		private ConversationState _state;

		public Conversation(IEnumerable<ISessionFactory> sessionFactories)
		{
			_state = ConversationState.Closed;
			_map = new Dictionary<ISessionFactory, ISession>();
			foreach(ISessionFactory sessionFactory in sessionFactories)
			{
				_map.Add(sessionFactory, null);
			}
		}

		public void Open()
		{
			CheckState(ConversationState.Closed);
			foreach(ISessionFactoryImplementor sessionFactory in _map.Keys.ToArray())
			{
				ISession session = sessionFactory.OpenSession(null, false, false, sessionFactory.Settings.ConnectionReleaseMode);
				session.FlushMode = FlushMode.Never;
				_map[sessionFactory] = session;
			}
			_state = ConversationState.Opened;
		}

		public IDisposable Context()
		{
			CheckState(ConversationState.Opened);
			foreach(ISession session in _map.Values)
			{
				session.BeginTransaction();
			}
			ConversationSessionContext.Bind(_map);
			_state = ConversationState.InContext;
			return new DisposeAction(UnbindContext);
		}

		public void UnbindContext()
		{
			CheckState(ConversationState.InContext);
			ConversationSessionContext.UnBind(_map);
			foreach(ISession session in _map.Values)
			{
				session.Transaction.Commit();
			}
			_state = ConversationState.Opened;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Close()
		{
			CheckState(ConversationState.Opened);
			foreach(ISessionFactory sessionFactory in _map.Keys.ToArray())
			{
				_map[sessionFactory].Close();
				_map[sessionFactory].Dispose();
				_map[sessionFactory] = null;
			}
			_state = ConversationState.Closed;
		}

		public void Flush()
		{
			CheckState(ConversationState.Opened);
			foreach(ISession session in _map.Values)
			{
				using(ITransaction tx = session.BeginTransaction())
				{
					session.Flush();
					tx.Commit();
				}
			}
		}

		private void CheckState(params ConversationState[] validStates)
		{
			foreach(ConversationState validState in validStates)
			{
				if(_state == validState)
				{
					return;
				}
			}
			throw new ConversationException(string.Concat("Operation not allowed in ", _state, " state"));
		}

		protected void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_state == ConversationState.InContext)
				{
					UnbindContext();
				}
				if(_state == ConversationState.Opened)
				{
					Close();
				}
			}
		}

		~Conversation()
		{
			Dispose(false);
		}
	}
}