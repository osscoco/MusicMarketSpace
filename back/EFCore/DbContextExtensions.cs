namespace EFCore
{
    public static class DbContextExtensions
    {
        public static void RemoveById<T>(this AppDbContext context, object id) where T : class
        {
            var entity = context.Set<T>().Find(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}