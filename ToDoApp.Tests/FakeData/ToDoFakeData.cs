using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Tests.FakeData
{
    public class ToDoFakeData
    {
        public List<ToDoItem> GetToDoItems()
        {
            return new List<ToDoItem>()
            {
                new ToDoItem()
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Clean up house",
                    Description = "All rooms",
                    CategoryId = new Guid("11111111-0000-0000-0000-111111111111"),
                    Category = new ToDoCategory()
                    {
                        Id = new Guid("00000000-1111-1111-1111-000000000000"),
                        Name = "Home",
                        ShortName = "HM"
                    },
                    Priority = ToDoPriority.High,
                    SubTasks = new List<SubTask>()
                    {
                        new SubTask()
                        {
                            Id = new Guid("00000000-1111-0000-1111-000000000000"),
                            Name = "Clean windows",
                            Description = "All windows",
                            IsDone = false,
                            ToDoItemId = new Guid("11111111-1111-1111-1111-111111111111")
                        }
                    },
                    CreationDate = DateTime.Now,
                    EndDate = new DateTime(2022, 3, 5)
                },
                new ToDoItem()
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "Study",
                    Description = "Docker",
                    CategoryId = new Guid("22222222-3333-3333-3333-222222222222"),
                    Category = new ToDoCategory()
                    {
                        Id = new Guid("22222222-3333-3333-3333-222222222222"),
                        Name = "Learn",
                        ShortName = "LRN"
                    },
                    Priority = ToDoPriority.Medium,
                    SubTasks = new List<SubTask>(),
                    CreationDate = DateTime.Now,
                    EndDate = new DateTime(2022, 4, 10)
                },
                new ToDoItem()
                {
                    Id = new Guid("33333333-3333-3333-3333-333333333333"),
                    Name = "Read book",
                    Description = "Sample book title",
                    CategoryId = new Guid("22222222-3333-3333-3333-222222222222"),
                    Category = new ToDoCategory()
                    {
                        Id = new Guid("22222222-3333-3333-3333-222222222222"),
                        Name = "Learn",
                        ShortName = "LRN"
                    },
                    Priority = ToDoPriority.Medium,
                    SubTasks = new List<SubTask>()
                    {
                        new SubTask()
                        {
                            Id = new Guid("22222222-1111-2222-1111-222222222222"),
                            Name = "Count of pages",
                            Description = "20 pages Monday",
                            IsDone = false,
                            ToDoItemId = new Guid("33333333-3333-3333-3333-333333333333")
                        }
                    },
                    CreationDate = DateTime.Now,
                    EndDate = new DateTime(2022, 3, 23)
                },
            };
        }

        public List<ToDoCategory> GetToDoCategories()
        {
            return new List<ToDoCategory>()
            {
                new ToDoCategory()
                {
                    Id = new Guid("00000000-1111-1111-1111-000000000000"),
                    Name = "Home",
                    ShortName = "HM"
                },
                new ToDoCategory()
                {
                    Id = new Guid("22222222-3333-3333-3333-222222222222"),
                    Name = "Learn",
                    ShortName = "LRN"
                },
            };
        }

        public List<SubTask> GetSubTasks()
        {
            return new List<SubTask>()
            {
                new SubTask()
                {
                    Id = new Guid("00000000-1111-0000-1111-000000000000"),
                    Name = "Clean windows",
                    Description = "All windows",
                    IsDone = false,
                    ToDoItemId = new Guid("11111111-1111-1111-1111-111111111111")
                },
                new SubTask()
                {
                    Id = new Guid("22222222-1111-2222-1111-222222222222"),
                    Name = "Count of pages",
                    Description = "20 pages Monday",
                    IsDone = false,
                    ToDoItemId = new Guid("33333333-3333-3333-3333-333333333333")
                }
            };
        }
    }
}
