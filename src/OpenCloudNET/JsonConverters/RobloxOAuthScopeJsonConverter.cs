using BloxTeams.OpenCloud.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.JsonConverters
{
    public class RobloxOAuthScopeJsonConverter : JsonConverter<RobloxOAuthScope>
    {
        private readonly Dictionary<RobloxOAuthScope, string> ScopeEnumMap = new()
        {
            { RobloxOAuthScope.OpenId, "openid" },
            { RobloxOAuthScope.Profile, "profile" },
            { RobloxOAuthScope.AssetRead, "asset:read" },
            { RobloxOAuthScope.AssetWrite, "asset:write" },
            { RobloxOAuthScope.UserAdvancedRead, "user.advanced:read" },
            { RobloxOAuthScope.UserSocialRead, "user.social:read" },
            { RobloxOAuthScope.UniverseWrite, "universe:write" },
            { RobloxOAuthScope.UniversePlaceWrite, "universe.place:write" },
            { RobloxOAuthScope.UserInventoryItemRead, "user.inventory-item:read" },
            { RobloxOAuthScope.GroupRead, "group:read" },
            { RobloxOAuthScope.GroupWrite, "group:write" },
            { RobloxOAuthScope.UserUserNotificationWrite, "user.user-notification:write" },
            { RobloxOAuthScope.CreatorStoreProductRead, "creator-store-product:read" },
            { RobloxOAuthScope.CreatorStoreProductWrite, "creator-store-product:write" },
            { RobloxOAuthScope.UniverseUserRestrictionRead, "universe.user-restriction:read" },
            { RobloxOAuthScope.UniverseUserRestrictionWrite, "universe.user-restriction:write" },
            { RobloxOAuthScope.UniverseSubscriptionProductSubscriptionRead, "universe.subscription-product.subscription:read" },
            { RobloxOAuthScope.UserCommerceItemRead, "user.commerce-item:read" },
            { RobloxOAuthScope.UserCommerceItemWrite, "user.commerce-item:write" },
            { RobloxOAuthScope.UserCommerceMerchantConnectionRead, "user.commerce-merchant-connection:read" },
            { RobloxOAuthScope.UserCommerceMerchantConnectionWrite, "user.commerce-merchant-connection:write" },
            { RobloxOAuthScope.LegacyUniverseBadgeWrite, "legacy-universe.badge:write" },
            { RobloxOAuthScope.LegacyBadgeManage, "legacy-badge:manage" },
            { RobloxOAuthScope.LegacyDeveloperProductManage, "legacy-developer-product:manage" },
            { RobloxOAuthScope.LegacyGamePassManage, "legacy-game-pass:manage" },
            { RobloxOAuthScope.LegacyUniverseFollowingRead, "legacy-universe.following:read" },
            { RobloxOAuthScope.LegacyUniverseFollowingWrite, "legacy-universe.following:write" },
            { RobloxOAuthScope.LegacyUniverseManage, "legacy-universe:manage" },
            { RobloxOAuthScope.LegacyGroupManage, "legacy-group:manage" },
            { RobloxOAuthScope.LegacyUserManage, "legacy-user:manage" },
            { RobloxOAuthScope.LegacyTeamCollaborationManage, "legacy-team-collaboration:manage" },
            { RobloxOAuthScope.UniverseMessagingServicePublish, "universe-messaging-service:publish" }
        };

        public override RobloxOAuthScope Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Find the mapped enum values

            string[] scopes = reader.GetString()!.Split(" ");

            RobloxOAuthScope scope = new RobloxOAuthScope();

            for (int i = 0; i < scopes.Length; i++)
            {
                string scopeName = scopes[i];

                scope |= ScopeEnumMap.Where(mapping => mapping.Value == scopeName).First().Key;
            }

            return scope;
        }

        public override void Write(Utf8JsonWriter writer, RobloxOAuthScope value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
         
            // Write the mapped string value for the chosen flags
            foreach (RobloxOAuthScope scope in Enum.GetValues(value.GetType()))
            {
                if (!value.HasFlag(scope)) continue;

                string scopeName = ScopeEnumMap.GetValueOrDefault(scope) ??
                    throw new Exception($"Could not find mapped value for {scope.ToString()}");

                writer.WriteStringValue(scopeName);
            }

            writer.WriteEndArray();
        }
    }
}
