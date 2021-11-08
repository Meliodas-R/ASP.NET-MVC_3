using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DaoEntityProducto : IDaoProducto
    {
        public void Borrar(long id)
        {
            using (TiendaMVCContext db = new TiendaMVCContext())
            {
                db.Productos.Remove(db.Productos.Find(id));
                db.SaveChanges();
            }
        }

        public Producto Insertar(Producto producto)
        {
            using (TiendaMVCContext db = new TiendaMVCContext())
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return producto;
            }
        }

        public Producto Modificar(Producto producto)
        {
            using (TiendaMVCContext db = new TiendaMVCContext())
            {
                db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return producto;
            }
        }

        public Producto ObtenerPorId(long id)
        {
            using (TiendaMVCContext db = new TiendaMVCContext())
            {
                return db.Productos.Find(id);
            }
        }

        public IEnumerable<Producto> ObtenerTodos()
        {

            using (TiendaMVCContext db = new TiendaMVCContext())
            {
                return db.Productos.ToList();
            }

        }
    }
}
