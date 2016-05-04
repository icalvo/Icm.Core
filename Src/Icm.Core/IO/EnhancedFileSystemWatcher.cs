using System;
using System.Diagnostics;
using System.IO;

namespace Icm.IO
{

    public class EnhancedFileSystemWatcher : FileSystemWatcher
    {

        protected FileSystemEventArgs LastDeletion { get; set; }

        protected DateTime LastDeletionDate { get; set; }

        protected DateTime Ahora { get; set; }

        public event EventHandler<FileSystemEventArgs> Creado;
        public event EventHandler<FileSystemEventArgs> Modificado;
        public event EventHandler<FileSystemEventArgs> Borrado;
        public event EventHandler<RenamedEventArgs> Renombrado;
        public event EventHandler<MovementEventArgs> Movido;

        public EnhancedFileSystemWatcher(string path) : base(path)
        {
            Renamed += base_Renamed;
            Deleted += base_Changed;
            Created += base_Changed;
            Changed += base_Changed;
            _tBorrado = new System.Timers.Timer(15) { AutoReset = false };
            _tBorrado.Elapsed += ChequearÚltimoBorrado;
        }

        private void base_Changed(object sender, FileSystemEventArgs e)
        {
            Ahora = DateTime.Now;
            Debug.WriteLine($"Changed {e.ChangeType} {e.FullPath}");
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Deleted:
                case WatcherChangeTypes.Created:
                    ChequearMovimiento(e);
                    break;
                case WatcherChangeTypes.Changed:
                    Modificado?.Invoke(sender, e);

                    break;
                case WatcherChangeTypes.Renamed:
                case WatcherChangeTypes.All:
                    break;
            }
        }

        private void base_Renamed(object sender, RenamedEventArgs e)
        {
            Renombrado?.Invoke(sender, e);
        }


        private readonly System.Timers.Timer _tBorrado;
        private void ChequearÚltimoBorrado(object s, System.Timers.ElapsedEventArgs e)
        {
            if ((LastDeletion != null))
            {
                Borrado?.Invoke(this, LastDeletion);
                LastDeletion = null;
            }
        }

        private void ChequearMovimiento(FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Deleted:
                    _tBorrado.Stop();
                    // Si tenemos otro borrado deberemos lanzar
                    // el evento del mismo (aunque eso suponga perder
                    // un posible movimiento).
                    if ((LastDeletion != null))
                    {
                        Borrado?.Invoke(this, LastDeletion);
                    }
                    // Ahora anotamos el evento de borrado actual
                    LastDeletion = e;
                    LastDeletionDate = DateTime.Now;
                    // Ponemos en marcha el temporizador. Si se trata
                    // realmente de un borrado, se disparará.
                    _tBorrado.Start();
                    break;
                case WatcherChangeTypes.Created:
                    _tBorrado.Stop();
                    if (Ahora.Subtract(LastDeletionDate).Milliseconds < 10)
                    {
                        if ((LastDeletion != null))
                        {
                            Movido?.Invoke(this, new MovementEventArgs(LastDeletion.FullPath, e.FullPath));
                        }
                        LastDeletion = null;
                    }
                    else
                    {
                        if ((LastDeletion != null))
                        {
                            Borrado?.Invoke(this, LastDeletion);
                            LastDeletion = null;
                        }
                        Creado?.Invoke(this, e);
                    }
                    break;
                case WatcherChangeTypes.Renamed:
                case WatcherChangeTypes.Changed:
                case WatcherChangeTypes.All:
                    break;
                default:
                    break;
            }
        }


    }

}
