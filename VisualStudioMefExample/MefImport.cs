using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace VisualStudioMefExample {
    public class MefImport {
        /// <summary>
        /// Here we import every single [Export] of type <see cref="IMessageWriter"/>
        /// See <see cref="ConsoleMessageWriter"/> and <see cref="ConsoleWithTraceOrDebugWriter"/>.
        /// </summary>
        [ImportMany]
        public IEnumerable<IMessageWriter> Writers { get; set; } = [];

        /// <summary>
        /// Execute every single message writer from <see cref="Writers"/>
        /// </summary>
        public static void ExecuteMefTasks() {
            // Create a new CompositionContainer and add parts from the current assembly
            var catalog = new AssemblyCatalog(typeof(MefImport).Assembly);
            var container = new CompositionContainer(catalog);

            // Compose parts
            var program = new MefImport();
            container.ComposeParts(program);

            // Use the imported parts
            foreach (var writer in program.Writers) {
                writer.Write("Hello, MEF with multiple writers!");
            }

            // So, what in general happens here, is VS MEF goes through every
            // type in the current assembly (typeof(MefImport).Assembly) and looks
            // through the types that has [ExportAttribute]. When we compose these
            // parts, MEF looks through every single enumerable with [Import] or [ImportMany]
            // attribute in the given class instance and puts every instance with [ExportAttribute]
            // in there.
            // This allows writing less code when it comes to instantiating multiple objects,
            // as with the powers of VS MEF, Visual Studio can automatically discover the needed
            // exports for us.
            // This can also come in handy during interaction with the application without the modification
            // of the source code (eg code from a separate module), which can be useful if you want your
            // application to support plugins.
        }
    }
}
