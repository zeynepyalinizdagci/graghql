using GraphQLDemo.GraphQL;
using GraphQLDemo.Repositories.Concretes;
using GraphQLDemo.Repositories.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services
            .AddGraphQLServer()
            .AddSorting()
            .AddQueryType<UserQuery>()
            .AddMutationType<UserMutation>()
            .AddType<FunctionalGroup>()
            //.AddDataLoader<FunctionalGroupDataLoader>()
            .AddMaxExecutionDepthRule(5); //depth limiting solve for instance set limit as 5 

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // app.UseRouting();
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapGraphQL();
        //     endpoints.MapGet("/", context => context.Response.Redirect("/graphql/playground", true));
        // });

        app.MapGraphQL("/graphql");
        app.MapControllers();

        app.Run();
    }
}