# Content



## Get content at root

```
GET /umbraco/Limbo/Migrations/GetContentAtRoot?maxLevel={maxLevel}
```

**Parameters:**

- `maxLevel` (optional)
The maximal level of children/descendants to return.



## Get content by ID

```
GET /umbraco/Limbo/Migrations/GetContentById?id={id}&maxLevel={maxLevel}
```

**Parameters:**

- `id` (required)
The ID of the content node.

- `maxLevel`
The maximal level of children/descendants to return.



## Get content by key

```
GET /umbraco/Limbo/Migrations/GetContentByKey?key={key}&maxLevel={maxLevel}
```

- `key` (required)
The GUID key of the content node.

- `maxLevel`
The maximal level of children/descendants to return.

