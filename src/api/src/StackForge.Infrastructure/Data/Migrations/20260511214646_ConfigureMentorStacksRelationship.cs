using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackForge.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureMentorStacksRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentorProfileStack_mentors_MentorsId",
                table: "MentorProfileStack");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorProfileStack_stacks_StacksId",
                table: "MentorProfileStack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MentorProfileStack",
                table: "MentorProfileStack");

            migrationBuilder.RenameTable(
                name: "MentorProfileStack",
                newName: "mentor_profile_stack");

            migrationBuilder.RenameColumn(
                name: "StacksId",
                table: "mentor_profile_stack",
                newName: "stack_id");

            migrationBuilder.RenameColumn(
                name: "MentorsId",
                table: "mentor_profile_stack",
                newName: "mentor_id");

            migrationBuilder.RenameIndex(
                name: "IX_MentorProfileStack_StacksId",
                table: "mentor_profile_stack",
                newName: "IX_mentor_profile_stack_stack_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mentor_profile_stack",
                table: "mentor_profile_stack",
                columns: new[] { "mentor_id", "stack_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_mentor_profile_stack_mentors_mentor_id",
                table: "mentor_profile_stack",
                column: "mentor_id",
                principalTable: "mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mentor_profile_stack_stacks_stack_id",
                table: "mentor_profile_stack",
                column: "stack_id",
                principalTable: "stacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mentor_profile_stack_mentors_mentor_id",
                table: "mentor_profile_stack");

            migrationBuilder.DropForeignKey(
                name: "FK_mentor_profile_stack_stacks_stack_id",
                table: "mentor_profile_stack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mentor_profile_stack",
                table: "mentor_profile_stack");

            migrationBuilder.RenameTable(
                name: "mentor_profile_stack",
                newName: "MentorProfileStack");

            migrationBuilder.RenameColumn(
                name: "stack_id",
                table: "MentorProfileStack",
                newName: "StacksId");

            migrationBuilder.RenameColumn(
                name: "mentor_id",
                table: "MentorProfileStack",
                newName: "MentorsId");

            migrationBuilder.RenameIndex(
                name: "IX_mentor_profile_stack_stack_id",
                table: "MentorProfileStack",
                newName: "IX_MentorProfileStack_StacksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MentorProfileStack",
                table: "MentorProfileStack",
                columns: new[] { "MentorsId", "StacksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MentorProfileStack_mentors_MentorsId",
                table: "MentorProfileStack",
                column: "MentorsId",
                principalTable: "mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MentorProfileStack_stacks_StacksId",
                table: "MentorProfileStack",
                column: "StacksId",
                principalTable: "stacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
