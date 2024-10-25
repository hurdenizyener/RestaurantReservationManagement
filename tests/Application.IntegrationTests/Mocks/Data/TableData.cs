using Domain.Entities;

namespace Application.IntegrationTests.Mocks.Data;


internal static class TableData
{
    internal static List<Table> GetInitialData()
    {
        return
        [
            new Table { Id = Guid.NewGuid(), Number = 1, Capacity = 2, IsAvailable = true },
            new Table { Id = Guid.NewGuid(), Number = 2, Capacity = 4, IsAvailable = true },
            new Table { Id = Guid.NewGuid(), Number = 3, Capacity = 6, IsAvailable = true },
            new Table { Id = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7"), Number = 4, Capacity = 8, IsAvailable = false }
        ];
    }
}
