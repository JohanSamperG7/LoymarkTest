using System.ComponentModel;

namespace Loymark.Back.Domain.Enums
{
    public enum ActivityType
    {
        [Description("Creación de Usuario")]
        CreateUser,
        [Description("Actualización de Usuario")]
        UpdateUser,
        [Description("Borrado de Usuario")]
        DeleteUser,
    }
}
