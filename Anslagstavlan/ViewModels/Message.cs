using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.ViewModels
{
    public class Message
    {

        //maybe add a viewModel class to solve problems with details razor page

        [BindProperty]
        public string Text { get; set; }

    }
}
