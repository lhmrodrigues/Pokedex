﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex
{
    public abstract class Base
    {
        [PrimaryKey, Column("Id")]
        public int Id { get; set; }
    }
}
