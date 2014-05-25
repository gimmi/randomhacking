package com.github.gimmi.trackr.domain;

import java.util.UUID;

public abstract class BaseEntity {
    private String id = UUID.randomUUID().toString();

    protected BaseEntity(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    @Override
    public int hashCode() {
        return id.hashCode();
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (!(obj instanceof BaseEntity)) {
            return false;
        }
        BaseEntity other = (BaseEntity) obj;
        return id.equals(other.id);
    }
}
