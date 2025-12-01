# Bob-Shirt - C# Coding & Architecture Standards

**Purpose**: define internal, pragmatic style and architecture rules for a small project so all contributors maintain consistent, clear, and maintainable code.

---

## 1. High level principles

* **Clarity over cleverness.** Code should be readable and obvious.
* **Keep boundaries thin and explicit.** Modules expose narrow, intention-revealing APIs.
* **Prefer composition to inheritance.** Limit inheritance depth to two levels.
* **Minimize runtime polymorphism.** Seal classes when possible, use interfaces wisely.
* **Error handling.** Use exceptions for truly exceptional conditions; otherwise, consider `Result<T>`-style patterns.
* **Immutable data where practical.** Especially for cross-module communication.

---

## 2. Project & repository layout

Bob-Shirt is small and multi-target, with shared logic centralized in `Bob.Core`.

* `/Bob.Core/` -> shared logic, views, view models, assets
* `/Bob.Desktop/` -> desktop-specific bootstrap and platform glue
* `/Bob.WASM/` -> WASM host, web bootstrap, `wwwroot` assets
* `/docs/` -> architecture and dev notes
* `/scripts/` -> helper scripts
* `/vendor/` -> third-party binaries/utilities

Rules:

* One primary type per file; filename should be the type name.
* All cross-platform logic must live in `Bob.Core`.
* Desktop/WASM projects only contain platform-specific bootstrap code.

---

## 3. Namespaces and naming conventions

* Types: `PascalCase`
* Parameters/locals: `camelCase`
* Private fields: `m_PascalCase`
* Interfaces: `I` prefix
* Async methods: `Async` suffix
* Boolean methods/properties: `Is/Has/Can` prefix
* Constants: `UPPERCASE`
* Root namespace: `Bob` with sub-namespaces reflecting folder/project structure.

---

## 4. Architecture & dependency rules

* Layers: `Core` → `Desktop/WASM` → optional tooling.
* No cyclic dependencies.
* Lower-level modules cannot depend on higher-level modules.
* Dependency Injection allowed at composition root.
* Keep public API surface minimal and documented.
* Performance-conscious: avoid unnecessary heap allocations in hot paths, prefer `Span<T>`, `ReadOnlySpan<T>` and pooling.
* Concurrency: explicit thread ownership, avoid shared mutable state.

---

## 5. Coding practices

* 4-space indentation, max 120 characters.
* UTF-8, LF line endings.
* Enforce formatting via `dotnet format`.
* Documentation: XML comments for public APIs, inline comments for complex logic.
* Methods short and single-purpose.
* Async: avoid `async void` except for event handlers.
* Logging: small abstraction, inject logger, avoid noise in hot loops.

---

## 6. Commits & workflow

* **No branching**: all work is rebased onto `master`.
* **Rebasing enforced**: avoid merge commits to keep history clean.
* **Commit messages**: present tense, descriptive, optional body for context.

---

## 7. Tooling & automation

* `.editorconfig` for consistent style.
* Roslyn analyzers for static analysis.
* `dotnet format` pre-commit and in CI.
* CI runs build + tests + analyzers + format-check.
* Dependency management via NuGet; pin versions.

---

## 8. Code review checklist

* Code intent clear?
* Public APIs minimal and documented?
* Hot-path allocations acceptable?
* Logging appropriate?
* PR size manageable?
* Naming & layout follow standards?
