using System;
using System.Data;

namespace my_agenda
{
    class Agenda : Connection
    {
        private int id;
        private String nombre;
        private String telefono;

        public Agenda() { }

        public Agenda(int id)
        {
            this.id = id;
        }

        public Agenda(string nombre, string telefono)
        {
            this.nombre = nombre;
            this.telefono = telefono;
        }

        public Agenda(int id=0, string nombre="", string telefono="") : this(id)
        {
            this.nombre = nombre;
            this.telefono = telefono;
        }

        public Boolean insertar()
        {
            String sql = String.Format(
                "INSERT INTO directorio(nombre, telefono) VALUES('{0}', '{1}')", 
                nombre, 
                telefono
            );
            return base.insert(sql);
        }

        public DataTable consultar()
        {
            return base.query("SELECT * FROM directorio");
        }

        public Boolean eliminar()
        {
            String sql = String.Format("DELETE FROM directorio WHERE id={0}", id);
            return base.delete(sql);
        }

        public Boolean actualizar()
        {
            String sql = String.Format(
                "UPDATE directorio SET nombre='{1}', telefono='{2}' WHERE id={0}", 
                id, 
                nombre, 
                telefono
            );
            return base.update(sql);
        }
    }
}
