using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Ray.EssayNotes.MediatorDemo
{
    internal class MyCommand : IRequest<long>
    {
        public string CommandName { get; set; }
    }
}
