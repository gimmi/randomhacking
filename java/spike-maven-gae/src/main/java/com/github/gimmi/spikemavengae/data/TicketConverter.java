package com.github.gimmi.spikemavengae.data;

import java.util.List;

import com.github.gimmi.spikemavengae.domain.Comment;
import com.github.gimmi.spikemavengae.domain.Ticket;
import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.Key;
import com.google.appengine.repackaged.com.google.common.collect.Lists;

public class TicketConverter implements EntityConverter<Ticket> {
	@Override
	public Ticket fromEntity(Entity entity) {
		Ticket ret = new Ticket();
		ret.id = entity.getKey().getName();
		ret.title = entity.getProperty("title").toString();
		return ret;
	}

	@Override
	public Entity toEntity(Ticket obj, Key parent) {
		Entity e = new Entity(getKind(), obj.id, parent);
		e.setProperty("title", obj.title);
		return e;
	}

	@Override
	public Iterable<Object> getChildren(Ticket obj) {
		List<Object> ret = Lists.newArrayList();
		ret.addAll(obj.comments);
		return ret;
	}

	@Override
	public String getKind() {
		return Ticket.class.getSimpleName();
	}

	@Override
	public void setChild(Ticket obj, Object child) {
		obj.comments.add((Comment) child);
	}
}
