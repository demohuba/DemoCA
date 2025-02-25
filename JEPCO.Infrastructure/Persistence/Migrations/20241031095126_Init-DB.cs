using System;
using JEPCO.Infrastructure.Persistence.DbContextInterfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JEPCO.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customers");

            // move the postgis extension to the needed schema
            migrationBuilder.Sql(@$"UPDATE pg_extension 
                          SET extrelocatable = TRUE 
                            WHERE extname = 'postgis';
   
                        ALTER EXTENSION postgis 
                          SET SCHEMA customers;");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "audit_logs",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: false),
                    NewValues = table.Column<string>(type: "text", nullable: false),
                    AffectedColumns = table.Column<string>(type: "text", nullable: false),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_customer_meter_relation_types",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_customer_meter_relation_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_genders",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_languages",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_mobile_platform_types",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_mobile_platform_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_notification_types",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_notification_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_self_meter_read_sap_status",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_self_meter_read_sap_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lk_user_types",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NameAR = table.Column<string>(type: "text", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lk_user_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    GenderId = table.Column<int>(type: "integer", nullable: false),
                    PreferredLanguageId = table.Column<int>(type: "integer", nullable: false),
                    UserTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_lk_genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "customers",
                        principalTable: "lk_genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_lk_languages_PreferredLanguageId",
                        column: x => x.PreferredLanguageId,
                        principalSchema: "customers",
                        principalTable: "lk_languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_lk_user_types_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "customers",
                        principalTable: "lk_user_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "customers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_customers_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "customers",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_cloud_messaging_tokens",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenId = table.Column<string>(type: "text", nullable: false),
                    MobilePlatformId = table.Column<int>(type: "integer", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_cloud_messaging_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_cloud_messaging_tokens_lk_mobile_platform_types_Mobile~",
                        column: x => x.MobilePlatformId,
                        principalSchema: "customers",
                        principalTable: "lk_mobile_platform_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_cloud_messaging_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "customers",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_notification_preferences",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_notification_preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_notification_preferences_lk_notification_types_Notific~",
                        column: x => x.NotificationTypeId,
                        principalSchema: "customers",
                        principalTable: "lk_notification_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_notification_preferences_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "customers",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer_meters",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SapFileNo = table.Column<string>(type: "text", nullable: false),
                    RelationTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_meters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_meters_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customers",
                        principalTable: "customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customer_meters_lk_customer_meter_relation_types_RelationTy~",
                        column: x => x.RelationTypeId,
                        principalSchema: "customers",
                        principalTable: "lk_customer_meter_relation_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "self_meters_reads",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: true),
                    MeterNumber = table.Column<string>(type: "text", nullable: true),
                    FirstReadNumber = table.Column<long>(type: "bigint", nullable: true),
                    FirstReadImage = table.Column<string>(type: "text", nullable: true),
                    SecondReadNumber = table.Column<long>(type: "bigint", nullable: true),
                    SecondReadImage = table.Column<string>(type: "text", nullable: true),
                    SapStatusId = table.Column<int>(type: "integer", nullable: false),
                    ScanDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExportedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ExportedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstMeterReadVerified = table.Column<bool>(type: "boolean", nullable: true),
                    SecondMeterReadVerified = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_self_meters_reads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_self_meters_reads_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customers",
                        principalTable: "customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_self_meters_reads_lk_self_meter_read_sap_status_SapStatusId",
                        column: x => x.SapStatusId,
                        principalSchema: "customers",
                        principalTable: "lk_self_meter_read_sap_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_meters_CustomerId",
                schema: "customers",
                table: "customer_meters",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_meters_RelationTypeId",
                schema: "customers",
                table: "customer_meters",
                column: "RelationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_self_meters_reads_CustomerId",
                schema: "customers",
                table: "self_meters_reads",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_self_meters_reads_SapStatusId",
                schema: "customers",
                table: "self_meters_reads",
                column: "SapStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_user_cloud_messaging_tokens_MobilePlatformId",
                schema: "customers",
                table: "user_cloud_messaging_tokens",
                column: "MobilePlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_user_cloud_messaging_tokens_UserId",
                schema: "customers",
                table: "user_cloud_messaging_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_notification_preferences_NotificationTypeId",
                schema: "customers",
                table: "user_notification_preferences",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_notification_preferences_UserId",
                schema: "customers",
                table: "user_notification_preferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_GenderId",
                schema: "customers",
                table: "users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_users_PreferredLanguageId",
                schema: "customers",
                table: "users",
                column: "PreferredLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UserTypeId",
                schema: "customers",
                table: "users",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "customer_meters",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "self_meters_reads",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "user_cloud_messaging_tokens",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "user_notification_preferences",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_customer_meter_relation_types",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_self_meter_read_sap_status",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_mobile_platform_types",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_notification_types",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "users",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_genders",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_languages",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "lk_user_types",
                schema: "customers");
        }
    }
}
