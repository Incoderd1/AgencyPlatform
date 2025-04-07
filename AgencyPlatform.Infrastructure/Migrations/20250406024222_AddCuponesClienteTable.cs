using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgencyPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCuponesClienteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cupones_cliente",
                columns: table => new
                {
                    id_cupon_cliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_cupon = table.Column<long>(type: "bigint", nullable: false),
                    id_cliente = table.Column<int>(type: "integer", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fecha_uso = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    estado = table.Column<string>(type: "estado_cupon_cliente", nullable: false),
                    id_transaccion = table.Column<long>(type: "bigint", nullable: true),
                    puntos_canjeados = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cupones_cliente", x => x.id_cupon_cliente);
                    table.ForeignKey(
                        name: "FK_cupones_cliente_cupones_id_cupon",
                        column: x => x.id_cupon,
                        principalTable: "cupones",
                        principalColumn: "id_cupon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cupones_cliente_clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cupones_cliente_id_cliente",
                table: "cupones_cliente",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_cupones_cliente_estado",
                table: "cupones_cliente",
                column: "estado");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "cupones_cliente");
        }

    }
}
