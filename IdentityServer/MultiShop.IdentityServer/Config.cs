// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog"){Scopes = {"CatalogFullPermission", "CatalogReadPermission" }},
            new ApiResource("ResourceDiscount"){Scopes = { "DiscountFullPermission" }},
            new ApiResource("ResourceOrder"){Scopes = {"OrderFullPermission"}},
            new ApiResource("ResourceCargo"){Scopes = {"CargoFullPermission" },},
            new ApiResource("ResourceBasket"){Scopes = {"BasketFullPermission" },},
            new ApiResource("ResourceComment"){Scopes = {"CommentFullPermission" },},
            new ApiResource("ResourcePayment"){Scopes = {"PaymentFullPermission" },},
            new ApiResource("ResourceImage"){Scopes = {"ImageFullPermission" },},
            new ApiResource("ResourceOcelot"){Scopes = {"OcelotFullPermission" },},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("CatalogFullPermission","FullAuthorityForCatalogOperations"),
            new ApiScope("CatalogReadPermission","ReadingAuthorityForCatalogOperations"),
            new ApiScope("DiscountFullPermission","FullAuthorityForDiscountOperations"),
            new ApiScope("OrderFullPermission","FullAuthorityForOrderOperations"),
            new ApiScope("CargoFullPermission","FullAuthorityForCargoOperations"),
            new ApiScope("BasketFullPermission","FullAuthorityForBasketOperations"),
            new ApiScope("CommentFullPermission","FullAuthorityForCommentOperations"),
            new ApiScope("PaymentFullPermission","FullAuthorityForPaymentOperations"),
            new ApiScope("ImageFullPermission","FullAuthorityForImageOperations"),
            new ApiScope("OcelotFullPermission","FullAuthorityForOcelotOperations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor - Ziyaretçi
            new Client
            {
                ClientId="MultiShopVisitorId",
                ClientName="Multi Shop Visitor User",
                AllowedGrantTypes=GrantTypes.ClientCredentials,//Client işlemlerinde kullandığımız kimlik işlemi
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes={ "CatalogReadPermission", "CatalogFullPermission", "CommentFullPermission","ImageFullPermission", "OcelotFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName, }
            },

            //Manager - Yönetici
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "Multi Shop Manager User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //Giriş yapan kullanıcının şifresine göre çalışsın
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = {"CatalogReadPermission","CatalogFullPermision" , "BasketFullPermission", "CommentFullPermission", "DiscountFullPermission","PaymentFullPermission", "ImageFullPermission","OrderFullPermission", "OcelotFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile, }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "Multi Shop Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes={ "CatalogFullPermission", "CatalogReadPermission", "DiscountFullPermission", "OrderFullPermission","CargoFullPermission","BasketFullPermission", "CommentFullPermission","PaymentFullPermission","ImageFullPermission" ,"OcelotFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                },
                AccessTokenLifetime=600 //10 dakika sonra token süresi tükeniyor
            }
        };
    }
}