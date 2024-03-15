---
icon: fa-solid fa-gear
---

# Content Types


## Get content type by ID

Returns the content type with the specified `id`.

```
GET /umbraco/Limbo/Migrations/GetContentTypeById?id={id}
```

**Parameters:**

- `id` (required)
The numeric ID of the content type.



## Get content type by key

Returns the content type with the specified GUID `key`.

```
GET /umbraco/Limbo/Migrations/GetContentTypeByKey?key={key}
```

**Parameters:**

- `key` (required)
The GUID key of the content type.



## Get content type by alias

Returns the content type with the specified `alias`.

```
GET /umbraco/Limbo/Migrations/GetContentTypeByAlias?alias={alias}
```

**Parameters:**

- `alias` (required)
The alias of the content type.