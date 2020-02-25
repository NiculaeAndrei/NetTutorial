﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
           : base(options)
        {

        }

        //public TodoContext() { }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
