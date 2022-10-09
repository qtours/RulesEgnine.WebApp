// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.WebApp.engine.factory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataCore();
builder.Services.AddControllers();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseMvcWithDefaultRoute();
app.Run();