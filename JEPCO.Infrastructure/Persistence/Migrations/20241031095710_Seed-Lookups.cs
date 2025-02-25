using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JEPCO.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_customer_meter_relation_types",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "مالك", "Owner", 1 },
                    { 2, "مستأجر", "Tenant", 2 }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_genders",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "ذكر", "Male", 1 },
                    { 2, "أنثى", "Female", 2 }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_languages",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "العربية", "Arabic", 1 },
                    { 2, "الإنجليزية", "English", 2 }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_mobile_platform_types",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "Android", "Android", 1 },
                    { 2, "IOS", "IOS", 2 },
                    { 3, "Huawei", "Huawei", 3 },
                    { 4, "Other", "Other", 4 }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_notification_types",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "إشعارات التطبيق", "App Notifications", 1 },
                    { 2, "البريد الإلكتروني", "Email", 2 },
                    { 3, "الرسائل النصية", "SMS", 3 }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_self_meter_read_sap_status",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[] { 1, "قيد البدء", "Initiated", 1 });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "lk_user_types",
                columns: new[] { "Id", "NameAR", "NameEN", "Order" },
                values: new object[,]
                {
                    { 1, "مدير", "Admin", 1 },
                    { 2, "مستخدم", "Customer", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_customer_meter_relation_types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_customer_meter_relation_types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_genders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_genders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_mobile_platform_types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_mobile_platform_types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_mobile_platform_types",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_mobile_platform_types",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_notification_types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_notification_types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_notification_types",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_self_meter_read_sap_status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_user_types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "customers",
                table: "lk_user_types",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
