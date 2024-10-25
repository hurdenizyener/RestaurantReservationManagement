using Domain.Entities;
using Xunit;

namespace Domain.UnitTests;

public class TableTests
{
    [Fact]
    public void Table_Constructor_Should_Initialize_Default_Values()
    {
        var table = new Table();

        Assert.NotEqual(Guid.Empty, table.Id);
        Assert.NotNull(table.Reservations); 
        Assert.Empty(table.Reservations); 
    }

    [Fact]
    public void Table_Should_Create_With_Valid_Values()
    {
        var id = Guid.NewGuid();
        var number = 1;
        var capacity = 4;
        var isAvailable = true;

        var table = new Table(id, number, capacity, isAvailable);

        Assert.Equal(id, table.Id);
        Assert.Equal(number, table.Number);
        Assert.Equal(capacity, table.Capacity);
        Assert.True(table.IsAvailable);
    }

    [Fact]
    public void Table_Should_Generate_New_Id_If_Empty_Guid_Provided()
    {
        var id = Guid.Empty;
        var number = 2;
        var capacity = 4;
        var isAvailable = true;

        var table = new Table(id, number, capacity, isAvailable);

        Assert.NotEqual(Guid.Empty, table.Id);
    }

    [Fact]
    public void Table_Should_Have_Empty_Reservation_List_When_Created()
    {
        var id = Guid.NewGuid();
        var number = 3;
        var capacity = 4;
        var isAvailable = true;

        var table = new Table(id, number, capacity, isAvailable);

        Assert.NotNull(table.Reservations);
        Assert.Empty(table.Reservations);
    }
}
