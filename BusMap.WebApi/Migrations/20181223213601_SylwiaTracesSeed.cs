using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class SylwiaTracesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusStopTraces",
                columns: new[] { "Id", "BusStopId", "Date", "Hour" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 5, 0, 0) },
                    { 100, 20, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 27, 0, 0) },
                    { 101, 21, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 19, 0, 0) },
                    { 102, 21, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 22, 0, 0) },
                    { 103, 21, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 28, 0, 0) },
                    { 104, 21, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 32, 0, 0) },
                    { 105, 21, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 37, 0, 0) },
                    { 106, 22, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 37, 0, 0) },
                    { 107, 22, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 39, 0, 0) },
                    { 108, 22, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 41, 0, 0) },
                    { 109, 22, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 46, 0, 0) },
                    { 110, 22, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 51, 0, 0) },
                    { 111, 23, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 6, 0, 0) },
                    { 112, 23, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 10, 0, 0) },
                    { 113, 23, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 56, 0, 0) },
                    { 114, 23, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 51, 0, 0) },
                    { 99, 20, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 25, 0, 0) },
                    { 98, 20, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 21, 0, 0) },
                    { 97, 20, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 14, 0, 0) },
                    { 96, 20, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 17, 0, 0) },
                    { 80, 16, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 44, 0, 0) },
                    { 81, 17, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 47, 0, 0) },
                    { 82, 17, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 45, 0, 0) },
                    { 83, 17, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 52, 0, 0) },
                    { 84, 17, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 57, 0, 0) },
                    { 85, 17, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0) },
                    { 86, 18, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 45, 0, 0) },
                    { 115, 23, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 4, 0, 0) },
                    { 87, 18, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 48, 0, 0) },
                    { 89, 18, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 2, 0, 0) },
                    { 90, 18, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 8, 0, 0) },
                    { 91, 19, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 2, 0, 0) },
                    { 92, 19, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 8, 0, 0) },
                    { 93, 19, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 12, 0, 0) },
                    { 94, 19, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 15, 0, 0) },
                    { 95, 19, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 10, 0, 0) },
                    { 88, 18, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 54, 0, 0) },
                    { 79, 16, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 43, 0, 0) },
                    { 116, 24, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 7, 0, 0) },
                    { 118, 24, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 2, 0, 0) },
                    { 139, 28, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 58, 0, 0) },
                    { 140, 28, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 8, 0, 0) },
                    { 141, 29, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 51, 0, 0) },
                    { 142, 29, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 59, 0, 0) },
                    { 143, 29, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 3, 0, 0) },
                    { 144, 29, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 12, 0, 0) },
                    { 145, 29, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 7, 0, 0) },
                    { 146, 30, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 4, 0, 0) },
                    { 147, 30, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 8, 0, 0) },
                    { 148, 30, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 11, 0, 0) },
                    { 149, 30, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 15, 0, 0) },
                    { 150, 30, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 17, 0, 0) },
                    { 151, 31, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 14, 0, 0) },
                    { 152, 31, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 11, 0, 0) },
                    { 153, 31, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 23, 0, 0) },
                    { 138, 28, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 54, 0, 0) },
                    { 137, 28, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 50, 0, 0) },
                    { 136, 28, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 46, 0, 0) },
                    { 135, 27, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 48, 0, 0) },
                    { 119, 24, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 12, 0, 0) },
                    { 120, 24, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 14, 0, 0) },
                    { 121, 25, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 17, 0, 0) },
                    { 122, 25, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 15, 0, 0) },
                    { 123, 25, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 21, 0, 0) },
                    { 124, 25, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 24, 0, 0) },
                    { 125, 25, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 27, 0, 0) },
                    { 117, 24, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 6, 0, 0) },
                    { 126, 26, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 26, 0, 0) },
                    { 128, 26, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 32, 0, 0) },
                    { 129, 26, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 35, 0, 0) },
                    { 130, 26, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 38, 0, 0) },
                    { 131, 27, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 32, 0, 0) },
                    { 132, 27, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 37, 0, 0) },
                    { 133, 27, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 41, 0, 0) },
                    { 134, 27, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 45, 0, 0) },
                    { 127, 26, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 28, 0, 0) },
                    { 154, 31, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 28, 0, 0) },
                    { 78, 16, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 31, 0, 0) },
                    { 76, 16, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 38, 0, 0) },
                    { 22, 5, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 57, 0, 0) },
                    { 23, 5, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 8, 0, 0) },
                    { 24, 5, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 38, 0, 0) },
                    { 25, 5, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 13, 0, 0) },
                    { 26, 6, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 53, 0, 0) },
                    { 27, 6, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 51, 0, 0) },
                    { 28, 6, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 10, 0, 0) },
                    { 29, 6, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 14, 0, 0) },
                    { 30, 6, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 55, 0, 0) },
                    { 31, 7, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 13, 0, 0) },
                    { 32, 7, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 6, 0, 0) },
                    { 33, 7, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 53, 0, 0) },
                    { 34, 7, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 21, 0, 0) },
                    { 35, 7, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 18, 0, 0) },
                    { 36, 8, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 12, 0, 0) },
                    { 21, 5, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 46, 0, 0) },
                    { 20, 4, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 43, 0, 0) },
                    { 19, 4, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 31, 0, 0) },
                    { 18, 4, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 51, 0, 0) },
                    { 2, 1, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 13, 0, 0) },
                    { 3, 1, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 9, 0, 0) },
                    { 4, 1, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 52, 0, 0) },
                    { 5, 1, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 47, 0, 0) },
                    { 6, 2, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 13, 0, 0) },
                    { 7, 2, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 9, 0, 0) },
                    { 8, 2, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 32, 0, 0) },
                    { 37, 8, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 7, 0, 0) },
                    { 9, 2, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 16, 0, 0) },
                    { 11, 3, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 16, 0, 0) },
                    { 12, 3, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 21, 0, 0) },
                    { 13, 3, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 41, 0, 0) },
                    { 14, 3, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 38, 0, 0) },
                    { 15, 3, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 33, 0, 0) },
                    { 16, 4, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 37, 0, 0) },
                    { 17, 4, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 45, 0, 0) },
                    { 10, 2, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 43, 0, 0) },
                    { 77, 16, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 35, 0, 0) },
                    { 38, 8, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 24, 0, 0) },
                    { 40, 8, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 21, 0, 0) },
                    { 61, 13, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 7, 0, 0) },
                    { 62, 13, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 2, 0, 0) },
                    { 63, 13, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 55, 0, 0) },
                    { 64, 13, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 47, 0, 0) },
                    { 65, 13, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 19, 0, 0) },
                    { 66, 14, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 18, 0, 0) },
                    { 67, 14, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 14, 0, 0) },
                    { 68, 14, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 8, 0, 0) },
                    { 69, 14, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 23, 0, 0) },
                    { 70, 14, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 26, 0, 0) },
                    { 71, 15, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 21, 0, 0) },
                    { 72, 15, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 16, 0, 0) },
                    { 73, 15, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 13, 0, 0) },
                    { 74, 15, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 32, 0, 0) },
                    { 75, 15, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 37, 0, 0) },
                    { 60, 12, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 9, 0, 0) },
                    { 59, 12, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 46, 0, 0) },
                    { 58, 12, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 51, 0, 0) },
                    { 57, 12, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 12, 0, 0) },
                    { 41, 9, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 14, 0, 0) },
                    { 42, 9, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 18, 0, 0) },
                    { 43, 9, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 21, 0, 0) },
                    { 44, 9, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 13, 0, 0) },
                    { 45, 9, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 38, 0, 0) },
                    { 46, 10, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 33, 0, 0) },
                    { 47, 10, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 27, 0, 0) },
                    { 39, 8, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 28, 0, 0) },
                    { 48, 10, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 17, 0, 0) },
                    { 50, 10, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 51, 0, 0) },
                    { 51, 11, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 46, 0, 0) },
                    { 52, 11, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 38, 0, 0) },
                    { 53, 11, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 31, 0, 0) },
                    { 54, 11, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 53, 0, 0) },
                    { 55, 11, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 58, 0, 0) },
                    { 56, 12, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 7, 0, 0) },
                    { 49, 10, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 46, 0, 0) },
                    { 155, 31, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 39, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "BusStopTraces",
                keyColumn: "Id",
                keyValue: 155);
        }
    }
}
