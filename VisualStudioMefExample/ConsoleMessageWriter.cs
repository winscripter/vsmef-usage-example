using System.ComponentModel.Composition;

namespace VisualStudioMefExample {
    [Export(typeof(IMessageWriter))]
    public class ConsoleMessageWriter : IMessageWriter {
        public void Write(string message) {
            Console.WriteLine(message);
        }
    }
}
