package com.github.gimmi.trackr.domain;

import java.util.ArrayList;
import java.util.List;

public class ItemRepository {
    public Item get(String id) {
        return new Item(id);
    }

    public List<Item> find() {
        ArrayList<Item> ret = new ArrayList<>();
        ret.add(new Item("1"));
        ret.add(new Item("2"));
        ret.add(new Item("3"));
        return ret;
    }
}
