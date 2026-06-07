using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackForge.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMentoshipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mentorship_requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    learner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mentor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stack_id = table.Column<Guid>(type: "uuid", nullable: false),
                    goal = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    decided_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mentorship_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mentorship_requests_learners_learner_id",
                        column: x => x.learner_id,
                        principalTable: "learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mentorship_requests_mentors_mentor_id",
                        column: x => x.mentor_id,
                        principalTable: "mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mentorship_requests_stacks_stack_id",
                        column: x => x.stack_id,
                        principalTable: "stacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mentorship_requests_learner_id",
                table: "mentorship_requests",
                column: "learner_id");

            migrationBuilder.CreateIndex(
                name: "IX_mentorship_requests_mentor_id",
                table: "mentorship_requests",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_mentorship_requests_stack_id",
                table: "mentorship_requests",
                column: "stack_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mentorship_requests");
        }
    }
}
