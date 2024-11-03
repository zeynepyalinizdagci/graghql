## GraphQL API implementation first approach
- reference : https://chillicream.com/docs/hotchocolate/v13/

## necessary nuget packages : 
`dotnet add package HotChocolate.AspNetCore`
`dotnet add package HotChocolate.Data`
`dotnet add package HotChocolate.Types`
# interact database with ef core.  
`dotnet add package HotChocolate.Data` 
`dotnet add package HotChocolate.Data.EntityFramework`
# resovle N+1 problem 
`dotnet add package HotChocolate.Execution.DataLoader`
# like swagger in rest  interactive api visualasition and testing 
`dotnet add package HotChocolate.AspNetCore.Playground`
`dotnet add package HotChocolate.AspNetCore.Voyager`
# ef core same as rest
`dotnet add package Microsoft.EntityFrameworkCore`
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
`dotnet add package Microsoft.EntityFrameworkCore.Tools`

# authtorization 
`dotnet add package HotChocolate.AspNetCore.Authorization`

## if needed more query or mutations
```csharp
public static class UserSchemaExtensions
{
    public static IRequestExecutorBuilder AddUserSchema(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddType<UserQuery>()
            .AddType<UserMutation>();
    }
}

public static class ProductSchemaExtensions
{
    public static IRequestExecutorBuilder AddProductSchema(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddType<ProductQuery>()
            .AddType<ProductMutation>();
    }
}
builder.Services
    .AddGraphQLServer()
    .AddUserSchema()
    .AddProductSchema();
```

## as alternative to add more query or mutation if application small
``` csharp
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddType<UserQuery>()
        .AddType<ProductQuery>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddType<UserMutation>()
        .AddType<ProductMutation>();
```

``` typescript
// query-builder.ts
import { gql } from 'apollo-angular';

export class QueryBuilder {
  private query: string;
  private fields: string[];

  constructor() {
    this.query = '';
    this.fields = [];
  }

  addField(field: string): QueryBuilder {
    this.fields.push(field);
    return this;
  }

  build(): string {
    if (this.fields.length === 0) {
      throw new Error('No fields added to the query.');
    }
    this.query = `query { ${this.fields.join(', ')} }`;
    return gql`${this.query}`;
  }
}

// mutation-builder.ts
import { gql } from 'apollo-angular';

export class MutationBuilder {
  private mutation: string;
  private input: any;

  constructor() {
    this.mutation = '';
    this.input = {};
  }

  setMutationName(name: string): MutationBuilder {
    this.mutation = name;
    return this;
  }

  setInput(input: any): MutationBuilder {
    this.input = input;
    return this;
  }

  build(): string {
    if (!this.mutation) {
      throw new Error('Mutation name is required.');
    }
    const inputString = JSON.stringify(this.input).replace(/"([^"]+)":/g, '$1:'); // Adjust input formatting
    this.mutation = `mutation { ${this.mutation}(${inputString}) }`;
    return gql`${this.mutation}`;
  }
}


// user.service.ts
import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { QueryBuilder } from './query-builder';
import { MutationBuilder } from './mutation-builder';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private apollo: Apollo) {}

  getUsers() {
    const query = new QueryBuilder()
      .addField('users { id name email }')
      .build();

    return this.apollo.watchQuery({ query }).valueChanges;
  }

  createUser(name: string, email: string) {
    const mutation = new MutationBuilder()
      .setMutationName('createUser')
      .setInput({ input: { name, email } })
      .build();

    return this.apollo.mutate({ mutation });
  }
}
```
