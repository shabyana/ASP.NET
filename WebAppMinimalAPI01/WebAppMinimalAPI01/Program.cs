namespace WebAppMinimalAPI01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<TodoRepository>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // GET запрос с параметром id в URL, например: /todos/1
            app.MapGet("/todos/{id}", (int id, TodoRepository repo) =>
            {
                var item = repo.GetById(id);
                // ≈сли нашли Ч возвращаем объект, если нет Ч 404 ошибку
                return item is not null ? Results.Ok(item) : Results.NotFound();
            });

            // DELETE запрос: /todos/1
            app.MapDelete("/todos/{id}", (int id, TodoRepository repo) =>
            {
                var deleted = repo.Delete(id);

                // ≈сли удалили Ч возвращаем 204 (No Content), если нет Ч 404
                return deleted ? Results.NoContent() : Results.NotFound();
            });
            // PUT запрос: /todos/1
            app.MapPut("/todos/{id}", (int id, Todo inputItem, TodoRepository repo) =>
            {
                var success = repo.Update(id, inputItem);

                // ≈сли обновили Ч возвращаем 204 (No Content), если ID не найден Ч 404
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/todos", (TodoRepository repo) => repo.GetAll());
            // ѕринимаем объект, ID в нем может быть 0 или отсутствовать
            app.MapPost("/todos", (Todo item, TodoRepository repo) => {
                var createdItem = repo.Add(item);
                return Results.Created($"/todos/{createdItem.Id}", createdItem);
            });
            app.Run();
        }


         //  ласс модели
        public class Todo
        {
           public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public bool IsCompleted { get; set; } = false;
        }
        //  ласс-хранилище (–епозиторий)
        public class TodoRepository
        {
            private readonly List<Todo> _todos = new();
            private int _nextId = 1; // —четчик дл€ ID
            public List<Todo> GetAll() => _todos;
            public Todo Add(Todo item)
            {
                item.Id = _nextId++; // ѕрисваиваем текущий ID и увеличиваем счетчик
                _todos.Add(item);
                return item;
            }
            public Todo? GetById(int id)
            {
                // »щем первый элемент с подход€щим ID или возвращаем null
                return _todos.FirstOrDefault(t => t.Id == id);
            }
            public bool Delete(int id)
            {
                var item = GetById(id);
                if (item is null) return false;

                _todos.Remove(item);
                return true;
            }
            public bool Update(int id, Todo updatedItem)
            {
                var existingItem = GetById(id);
                if (existingItem is null) return false;
                // ќбновл€ем пол€ существующего объекта
                existingItem.Title = updatedItem.Title;
                existingItem.IsCompleted = updatedItem.IsCompleted;

                return true;
            }

        }

    }
}
