using Supabase;

namespace Backend.Services.Storage;

public class SupabaseStorageClient(Client client)
{
    public Client Client { get; } = client;
}
