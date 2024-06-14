using System.ComponentModel.Composition;
using System.Text;

namespace VisualStudioMefExample {
    [Export(typeof(IMessageWriter))]
    public class ConsoleWithTraceOrDebugWriter : IMessageWriter {
        public void Write(string message) {
            string prefix =
#if DEBUG
            "Debug"
#else
            "Trace"
#endif
            ;
            var builder = new StringBuilder();
            builder.Append('[');
            builder.Append(prefix);
            builder.Append(']');
            builder.Append(": ");
            builder.Append(message);
            Console.WriteLine(builder.ToString());
        }
    }
}
