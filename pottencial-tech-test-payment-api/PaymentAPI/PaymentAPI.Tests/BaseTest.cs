namespace PaymentAPI.Tests;

public class BaseTest
{
  protected ApiContext _context;
  protected VendaRepository _repository;

  [SetUp]
  public async Task BeforeEach()
  {
    var options = new DbContextOptionsBuilder<ApiContext>()
        .UseInMemoryDatabase("PaymentAPI_Tests")
        .Options;
    _context = new ApiContext(options);
    _repository = new VendaRepository(_context);

    await _populate();
  }

  [TearDown]
  public async Task AfterEach()
  {
    await _context.Database.EnsureDeletedAsync();
  }

  private async Task _populate()
  {
    var vendedor = new VendedorRecord
    {
      Id = 1,
      Cpf = "65498732101",
      Nome = "John",
      Email = "john@mickeymouse.com",
      Telefone = "965654545"
    };
    var itens = new List<ItemRecord>();
    for (uint i = 1; i < 5; i++)
    {
      itens.Add(new ItemRecord
      {
        Nome = $"Item ${i}",
        PrecoUnitario = 17.5M,
        Quantidade = i + 1
      }
      );
    }
    for (uint i = 1; i < 4; i++)
    {
      await _repository.Insert(
        new VendaRecord
        {
          Id = i,
          Vendedor = vendedor,
          Itens = itens
        }
      );
    }
  }
}