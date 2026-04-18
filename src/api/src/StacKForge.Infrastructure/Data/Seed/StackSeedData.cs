using StackForge.Domain.Stacks.Entities;
using StackForge.Domain.Stacks.ValueObjects;

namespace StackForge.Infrastructure.Data.Seed
{
    public static class StackSeedData
    {
        public static IReadOnlyCollection<Stack> GetStacks() =>
        [
            Stack.Create("C#", Key.Create("csharp")),
            Stack.Create("Go", Key.Create("go")),
            Stack.Create("Node.js", Key.Create("nodejs")),
            Stack.Create("JavaScript", Key.Create("javascript")),
            Stack.Create("TypeScript", Key.Create("typescript")),
            Stack.Create("HTML", Key.Create("html")),
            Stack.Create("CSS", Key.Create("css")),
            Stack.Create(".NET", Key.Create("dotnet")),
            Stack.Create("Spring Boot", Key.Create("springboot")),
            Stack.Create("Django", Key.Create("django")),
            Stack.Create("Flask", Key.Create("flask")),
            Stack.Create("Express", Key.Create("express")),
            Stack.Create("Angular", Key.Create("angular")),
            Stack.Create("React", Key.Create("react")),
            Stack.Create("Vue.js", Key.Create("vue")),
            Stack.Create("Next.js", Key.Create("nextjs")),
            Stack.Create("Svelte", Key.Create("svelte")),
            Stack.Create("Unity", Key.Create("unity")),
            Stack.Create("Unreal Engine", Key.Create("unreal")),
            Stack.Create("Godot", Key.Create("godot")),
            Stack.Create("Flutter", Key.Create("flutter")),
            Stack.Create("React Native", Key.Create("react-native")),
            Stack.Create("Kotlin", Key.Create("kotlin")),
            Stack.Create("Swift", Key.Create("swift")),
            Stack.Create("Networking", Key.Create("networking")),
            Stack.Create("Cybersecurity", Key.Create("cybersecurity")),
            Stack.Create("Linux", Key.Create("linux")),
            Stack.Create("Docker", Key.Create("docker")),
            Stack.Create("Rust", Key.Create("rust")),
            Stack.Create("Pandas", Key.Create("pandas"))

        ];
    }
}
