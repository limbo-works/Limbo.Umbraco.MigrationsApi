---
icon: fa-solid fa-photo-film
---

# Media



## Get media at root

Returns a list of media items at the root level.

```
GET /umbraco/Limbo/Migrations/GetMediaAtRoot?maxLevel={maxLevel}
```

**Parameters:**

- `maxLevel` (optional)
The maximal level of children/descendants to return.



## Get media by ID

Returns the media item with the specified `id`.

```
GET /umbraco/Limbo/Migrations/GetMediaById?id={id}&maxLevel={maxLevel}
```

**Parameters:**

- `id` (required)
The ID of the media node.

- `maxLevel` (optional)
The maximal level of children/descendants to return.



## Get media by key

Returns the media item with the specified GUID `key`.

```
GET /umbraco/Limbo/Migrations/GetMediaByKey?key={key}&maxLevel={maxLevel}
```

**Parameters:**

- `key` (required)
The GUID key of the media node.

- `maxLevel` (optional)
The maximal level of children/descendants to return.

