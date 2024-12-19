using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLen2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
name: "Password",
table: "Account",
maxLength: 250,
nullable: true,
oldClrType: typeof(string),
oldType: "nvarchar(50)");
        }

 
       

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
     migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Account",
        maxLength: 50,
        nullable: true,
        oldClrType: typeof(string),
        oldMaxLength: 250);
        
        }
    }
}
