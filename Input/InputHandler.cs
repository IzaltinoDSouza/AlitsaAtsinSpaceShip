using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AASS
{
    class InputHandler
    {
        private Dictionary<Keys,ICommand> _commands;
        public InputHandler()
        {
            _commands = new Dictionary<Keys,ICommand>();
        }
        public void Bind(Keys key,ICommand command)
        {
            _commands.Add(key, command);
        }
        public void Unbind(Keys key)
        {
            _commands.Remove(key);
        }
        public void HandleInput()
        {
            foreach (var command in _commands)
            {
                if(Keyboard.GetState().IsKeyDown(command.Key))
                {
                    command.Value.Execute();
                }
            }
        }
    }
}