using ProjetoCRM.Repository.Context;

public abstract class BaseRepository
{
    protected readonly ProjetoCRMContext _context;

    protected BaseRepository(ProjetoCRMContext context)
    {
        _context = context;
    }
}