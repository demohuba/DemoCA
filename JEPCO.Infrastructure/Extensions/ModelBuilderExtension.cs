using JEPCO.Domain.Common;
using JEPCO.Domain.Entities;
using JEPCO.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace JEPCO.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {

        internal static ModelBuilder HandleSoftDeleteGlobally(this ModelBuilder modelBuilder)
        {
            Expression<Func<DeletableBaseEntity, bool>> filterExpr = bm => !bm.IsDeleted;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseAuditableEntity
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(DeletableBaseEntity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }

            return modelBuilder;
        }
        internal static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {

            // Create and Call any functions needed for seeding data
            SeedGenders(modelBuilder);
            SeedLanguages(modelBuilder);
            SeedPlatformTypes(modelBuilder);
            SeedNotificationTypes(modelBuilder);
            SeedSelfMetersReadSapStatus(modelBuilder);
            SeedUserTypes(modelBuilder);
            SeedCustomerMeterRelationTypes(modelBuilder);
            return modelBuilder;
        }

        //generate Default GL accounts Guid

        #region private method
        #region seeding methods

        private static void SeedGenders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_GenderEntity>().HasData(
                        new LK_GenderEntity() { Id = (int)GenderEnum.Male, NameEN = "Male", NameAR = "ذكر", Order = 1 },
                        new LK_GenderEntity() { Id = (int)GenderEnum.Female, NameEN = "Female", NameAR = "أنثى", Order = 2 }
                        );
        }
        private static void SeedLanguages(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_LanguageEntity>().HasData(
                        new LK_LanguageEntity() { Id = (int)LanguageEnum.ar, NameEN = "Arabic", NameAR = "العربية", Order = 1 },
                        new LK_LanguageEntity() { Id = (int)LanguageEnum.en, NameEN = "English", NameAR = "الإنجليزية", Order = 2 }
                        );
        }
        private static void SeedPlatformTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_MobilePlatformTypeEntity>().HasData(
                        new LK_MobilePlatformTypeEntity() { Id = (int)MobilePlatformTypeEnum.Android, NameEN = "Android", NameAR = "Android", Order = 1 },
                        new LK_MobilePlatformTypeEntity() { Id = (int)MobilePlatformTypeEnum.IOS, NameEN = "IOS", NameAR = "IOS", Order = 2 },
                        new LK_MobilePlatformTypeEntity() { Id = (int)MobilePlatformTypeEnum.Huawei, NameEN = "Huawei", NameAR = "Huawei", Order = 3 },
                        new LK_MobilePlatformTypeEntity() { Id = (int)MobilePlatformTypeEnum.Other, NameEN = "Other", NameAR = "Other", Order = 4 }
                        );
        }
        private static void SeedNotificationTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_NotificationTypeEntity>().HasData(
                        new LK_NotificationTypeEntity() { Id = (int)NotificationTypeEnum.PushNotification, NameEN = "App Notifications", NameAR = "إشعارات التطبيق", Order = 1 },
                        new LK_NotificationTypeEntity() { Id = (int)NotificationTypeEnum.Email, NameEN = "Email", NameAR = "البريد الإلكتروني", Order = 2 },
                        new LK_NotificationTypeEntity() { Id = (int)NotificationTypeEnum.SMS, NameEN = "SMS", NameAR = "الرسائل النصية", Order = 3 }
                        );
        }
        private static void SeedSelfMetersReadSapStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_SelfMeterReadSapStatusEntity>().HasData(
                        new LK_SelfMeterReadSapStatusEntity() { Id = (int)SelfMeterReadSapStatusEnum.Initiated, NameEN = "Initiated", NameAR = "قيد البدء", Order = 1 }
                        );
        }
        private static void SeedUserTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_UserTypeEntity>().HasData(
                        new LK_UserTypeEntity() { Id = (int)UserTypesEnum.Admin, NameEN = "Admin", NameAR = "مدير", Order = 1 },
                        new LK_UserTypeEntity() { Id = (int)UserTypesEnum.Customer, NameEN = "Customer", NameAR = "مستخدم", Order = 3 }
                        );
        }
        private static void SeedCustomerMeterRelationTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LK_CustomerMeterRelationTypeEntity>().HasData(
                        new LK_CustomerMeterRelationTypeEntity() { Id = (int)CustomerMeterRelationTypeEnum.Owner, NameEN = "Owner", NameAR = "مالك", Order = 1 },
                        new LK_CustomerMeterRelationTypeEntity() { Id = (int)CustomerMeterRelationTypeEnum.Tenant, NameEN = "Tenant", NameAR = "مستأجر", Order = 2 }
                        );
        }

        #endregion
        #endregion
    }
}
