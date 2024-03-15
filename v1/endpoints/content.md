# Content



## Get content at root

```
GET /umbraco/Limbo/Migrations/GetContentAtRoot
```

**Parameters:**

- `maxLevel` (optional)
The maximal level of children/descendants to return.



## Get content by ID

```
GET /umbraco/Limbo/Migrations/GetContentById
```

**Parameters:**

- `id` (required)
The ID of the content node.

- `maxLevel`
The maximal level of children/descendants to return.



## Get content by key

```
GET /umbraco/Limbo/Migrations/GetContentByKey
```

- `key` (required)
The GUID key of the content node.

- `maxLevel`
The maximal level of children/descendants to return.

