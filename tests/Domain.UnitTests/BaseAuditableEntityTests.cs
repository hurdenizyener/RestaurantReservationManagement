using Domain.Common;

namespace Domain.UnitTests;

public class AuditableEntityTestClass : BaseAuditableEntity { }
public class BaseAuditableEntityTests
{
    [Fact]
    public void Should_Set_CreatedDate_On_Initialization()
    {
        var createdDate = DateTimeOffset.Now;
        var entity = new AuditableEntityTestClass();

        Assert.NotEqual(createdDate, entity.CreatedDate); 
        Assert.True(entity.CreatedDate <= DateTimeOffset.Now);
    }

    [Fact]
    public void Should_Set_LastModifiedDate_On_Update()
    {
        var entity = new AuditableEntityTestClass
        {
            LastModifiedDate = DateTimeOffset.Now.AddMinutes(5)
        };

        Assert.NotNull(entity.LastModifiedDate);
        Assert.True(entity.LastModifiedDate > entity.CreatedDate); 
    }
}
