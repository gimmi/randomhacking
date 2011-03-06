package com.github.gimmi.spikemavengae.data;

import com.github.gimmi.spikemavengae.domain.Comment;
import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.Key;

public class CommentConverter implements EntityConverter<Comment> {
	@Override
	public String getKind() {
		return Comment.class.getSimpleName();
	}

	@Override
	public Comment fromEntity(Entity entity) {
		Comment ret = new Comment();
		ret.id = entity.getKey().getName();
		ret.user = entity.getProperty("user").toString();
		ret.text = entity.getProperty("text").toString();
		return ret;
	}

	@Override
	public Entity toEntity(Comment obj, Key parent) {
		Entity ret = new Entity(getKind(), obj.id, parent);
		ret.setProperty("user", obj.user);
		ret.setProperty("text", obj.text);
		return ret;
	}

	@Override
	public Iterable<Object> getChildren(Comment obj) {
		return null;
	}

	@Override
	public void setChild(Comment obj, Object child) {
	}
}
