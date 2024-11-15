namespace OpenCloud.Enums
{
    [Flags]
    public enum RobloxOAuthScope
    {
        OpenId = 1,
        Profile = 1 << 1,
        AssetRead = 1 << 2,
        AssetWrite = 1 << 3,
        UserAdvancedRead = 1 << 4,
        UserSocialRead = 1 << 5,
        UniverseWrite = 1 << 6,
        UniversePlaceWrite = 1 << 7,
        UserInventoryItemRead = 1 << 8,
        GroupRead = 1 << 9,
        GroupWrite = 1 << 10,
        UserUserNotificationWrite = 1 << 11,
        CreatorStoreProductRead = 1 << 12,
        CreatorStoreProductWrite = 1 << 13,
        UniverseUserRestrictionRead = 1 << 14,
        UniverseUserRestrictionWrite = 1 << 15,
        UniverseSubscriptionProductSubscriptionRead = 1 << 16,
        UserCommerceItemRead = 1 << 17,
        UserCommerceItemWrite = 1 << 18,
        UserCommerceMerchantConnectionRead = 1 << 19,
        UserCommerceMerchantConnectionWrite = 1 << 20,
        LegacyUniverseBadgeWrite = 1 << 21,
        LegacyBadgeManage = 1 << 22,
        LegacyDeveloperProductManage = 1 << 23,
        LegacyGamePassManage = 1 << 24,
        LegacyUniverseFollowingRead = 1 << 25,
        LegacyUniverseFollowingWrite = 1 << 26,
        LegacyUniverseManage = 1 << 27,
        LegacyGroupManage = 1 << 28,
        LegacyUserManage = 1 << 29,
        LegacyTeamCollaborationManage = 1 << 30,
        UniverseMessagingServicePublish = 1 << 31
    }
}
