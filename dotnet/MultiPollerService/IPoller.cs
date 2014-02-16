using System;

namespace MultiPollerService
{
	public interface IPoller
	{
		TimeSpan Poll();
	}
}