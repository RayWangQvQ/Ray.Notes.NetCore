using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Ray.EssayNotes.MediatorDemo
{
    internal class MyEvent : INotification
    {
        public string EventName { get; set; }
    }
}
