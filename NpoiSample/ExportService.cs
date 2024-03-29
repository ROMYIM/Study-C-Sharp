﻿using NPOI.XSSF.UserModel;
using NpoiSample.Models;

namespace NpoiSample;

public class ExportService
{
    private readonly IFreeSql _freeSql;

    private readonly string _filePath;

    private readonly string _sheetName;

    public ExportService(IFreeSql freeSql, string filePath, string sheetName)
    {
        _freeSql = freeSql;
        _filePath = filePath;
        _sheetName = sheetName;
    }

    public async Task ExportCustomerInfoAsync()
    {
        const int step = 100;
        // const int totalCount = 10;
        const int totalCount = 34179;
        
        await using var file = File.OpenRead(_filePath);
        var workbook = new XSSFWorkbook(file);
        var sheet = workbook.GetSheet(_sheetName) ?? workbook.GetSheetAt(0);
        
        var cellStyle = workbook.CreateCellStyle();
        cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
        
        var executeCounter = 0;
        for (var i = 3; i < totalCount + 3; i++)
        {
            var row = sheet.GetRow(i);
            var customerId = row.Cells[0].StringCellValue;
            var customerFullName = row.GetCell(1)?.StringCellValue;
        
            if (string.IsNullOrWhiteSpace(customerFullName))
            {
                // var customers = await freeSql.Select<CusCustomer>().WithLock()
                //     .From<SysUserInfo, SysUserInfo, BasCountry, BasCompany, BasCustomerFrom, BasDistrict>(
                //         (customerInfo, clueUser, businessUser, basCountry, basCompany, customerFrom, basDistrict) =>
                //             customerInfo.LeftJoin(customer => clueUser.UserId == customer.ClueUserId)
                //                 .LeftJoin(customer => businessUser.UserId == customer.BusiUserId)
                //                 .LeftJoin(customer => basCountry.CountryId == customer.CountryId)
                //                 .LeftJoin(customer => basCompany.CompanyId == customer.ManageCompany)
                //                 .LeftJoin(customer => customerFrom.FromId == customer.CustomerFrom)
                //                 .LeftJoin(customer => basDistrict.DistrictCode == customer.District))
                //     .Where((customer, clueUser, businessUser, basCountry, basCompany, customerFrom, basDistrict) =>
                //         customer.CustomerId == customerId)
                //     .ToListAsync((customer, clueUser, businessUser, basCountry, basCompany, customerFrom, basDistrict) =>
                //         new CustomerFollowUserInfoDto()
                //         {
                //             CustomerId = customer.CustomerId,
                //             AddressCity = customer.AddrCity,
                //             AddressDistrict = customer.AddrDistrict,
                //             AddressProvince = customer.AddrProvince,
                //             BusinessUser = businessUser.CnName,
                //             ClueUser = clueUser.CnName,
                //             Country = basCountry.CountryZhCn,
                //             CreateTime = customer.CreateTime,
                //             District = basDistrict.TextZhCn,
                //             Industry = customer.Industry,
                //             CustomerEnabled = customer.IsEnabled == true ? "是" : "否",
                //             CustomerFrom = customerFrom.FromNameZhCn,
                //             CustomerLevel = customer.TypeId,
                //             IsCooperation = customer.IsCooperation,
                //             ManageCompany = basCompany.ShortNameCn,
                //             CustomerFullName = customer.CustomerFullName,
                //         });
                
                var followUsers = await _freeSql.Select<CusCustomer>().WithLock()
                .From<CusFollowList, SysUserInfo, CustomerGrantUser, SysUserInfo, SysUserInfo, BasCountry, BasCompany,
                    BasCustomerFrom, BasDistrict>(
                    (customerInfo, customerFollow, userInfo, grantUser, clueUser, businessUser, basCountry, basCompany, customerFrom, basDistrict) =>
                        customerInfo.LeftJoin(customer => customerFollow.CustomerId == customer.CustomerId)
                            .LeftJoin(customer => customerFollow.TrackUser == userInfo.UserId)
                            .LeftJoin(customer => customerFollow.CustomerId == grantUser.CustomerId &&
                                                  customerFollow.TrackUser == grantUser.UserId)
                            .LeftJoin(customer => clueUser.UserId == customer.ClueUserId)
                            .LeftJoin(customer => businessUser.UserId == customer.BusiUserId)
                            .LeftJoin(customer => basCountry.CountryId == customer.CountryId)
                            .LeftJoin(customer => basCompany.CompanyId == customer.ManageCompany)
                            .LeftJoin(customer => customerFrom.FromId == customer.CustomerFrom)
                            .LeftJoin(customer => basDistrict.DistrictCode == customer.District))
                    .Where((customer, customerFollow, userInfo, grantUser, clueUser, businessUser, basCountry, basCompany,
                            customerFrom, basDistrict) =>
                        customer.CustomerId == customerId).ToListAsync((customer,
                        customerFollow,
                        userInfo, grantUser, clueUser, businessUser, basCountry, basCompany,
                        customerFrom, basDistrict) => new CustomerFollowUserInfoDto()
                        {
                            CustomerId = customerFollow.CustomerId,
                            AddressCity = customer.AddrCity,
                            AddressDistrict = customer.AddrDistrict,
                            AddressProvince = customer.AddrProvince,
                            BusinessUser = businessUser.CnName,
                            CaseDirection = customerFollow.CaseDirection ?? string.Empty,
                            CaseSourceClass = customerFollow.HeadUserType,
                            CaseType = customerFollow.CaseType ?? string.Empty,
                            ClueUser = clueUser.CnName,
                            Country = basCountry.CountryZhCn,
                            CreateTime = customer.CreateTime,
                            District = basDistrict.TextZhCn,
                            Industry = customer.Industry,
                            CustomerEnabled = customer.IsEnabled == true ? "是" : "否",
                            CustomerFrom = customerFrom.FromNameZhCn,
                            CustomerLevel = customer.TypeId,
                            IsCooperation = customer.IsCooperation,
                            ManageCompany = basCompany.ShortNameCn,
                            UserName = userInfo.CnName,
                            CustomerFullName = customer.CustomerFullName,
                            CustomerUserType = customerFollow.CustomerUserType,
                            FollowerEnabled = customerFollow.IsEnabled,
                            UserEnabled = userInfo.IsEnabled,
                            GrantType = grantUser.GrantType
                        });
                
                foreach (var dto in followUsers)
                {
                    // row.SetValue(cellStyle, 0, dto.CustomerId);
                    row.SetValue(cellStyle, 1, dto.CustomerFullName);
                    row.SetValue(cellStyle, 2, dto.CustomerEnabled);
                    if (dto.CaseType == "P")
                    {
                        dto.SetExcelFollowUserValue(row, cellStyle, 3);
                    }
                    
                    else if (dto.CaseType == "T")
                    {
                        dto.SetExcelFollowUserValue(row, cellStyle, 15);
                    }
                    
                    else if (dto.CaseType == "X")
                    {
                        dto.SetExcelFollowUserValue(row, cellStyle, 27);
                    }
                    
                    else if (dto.CaseType == "C")
                    {
                        dto.SetExcelFollowUserValue(row, cellStyle, 39);
                    }
                    
                    else if (dto.CaseType == "L")
                    {
                        dto.SetExcelFollowUserValue(row, cellStyle, 51);
                    }
                    row.SetValue(cellStyle, 63, dto.District);
                    row.SetValue(cellStyle, 64, dto.Country);
                    row.SetValue(cellStyle, 65, dto.AddressProvince);
                    row.SetValue(cellStyle, 66, dto.AddressCity);
                    row.SetValue(cellStyle, 67, dto.AddressDistrict);
                    row.SetValue(cellStyle, 68, dto.CustomerFrom);
                    row.SetValue(cellStyle, 69, dto.GetCustomerLevel);
                    row.SetValue(cellStyle, 70, dto.Industry);
                    row.SetValue(cellStyle, 71, dto.ManageCompany);
                    row.SetValue(cellStyle, 72, dto.ClueUser);
                    row.SetValue(cellStyle, 73, dto.BusinessUser);
                    row.SetValue(cellStyle, 74, dto.CreateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty);
                    var isCooperation = dto.IsCooperation == null ? string.Empty : (dto.IsCooperation.Value ? "是" : "否");
                    row.SetValue(cellStyle, 75, isCooperation);
                }
            
                if (executeCounter < step)
                {
                    executeCounter++;
                }
                else
                {
                    await using var fileWrite = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                    workbook.Write(fileWrite);
                    executeCounter = 0;
                }
            }
        
        
        }
        await using var lastWrite = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
        workbook.Write(lastWrite);
        workbook.Close();
    }
}