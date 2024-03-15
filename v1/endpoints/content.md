# Content



## Get content at root

Returns a list of content items at the root level.

```
GET /umbraco/Limbo/Migrations/GetContentAtRoot?maxLevel={maxLevel}
```

**Parameters:**

- `maxLevel` (optional)
The maximal level of children/descendants to return.



## Get content by ID

Returns the content item with the specified `id`.

```
GET /umbraco/Limbo/Migrations/GetContentById?id={id}&maxLevel={maxLevel}
```

**Parameters:**

- `id` (required)
The ID of the content node.

- `maxLevel`
The maximal level of children/descendants to return.



## Get content by key

Returns the content item with the specified GUID `key`.

```
GET /umbraco/Limbo/Migrations/GetContentByKey?key={key}&maxLevel={maxLevel}
```

- `key` (required)
The GUID key of the content node.

- `maxLevel`
The maximal level of children/descendants to return.

