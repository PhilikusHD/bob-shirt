# Bob-Shirt - Architectural Layers

## 1. Layer Overview

Bob-Shirt follows a four-layer architecture:

1. **UI Layer**
2. **Service Layer**
3. **Repository Layer**
4. **Domain Layer**

Each layer has a specific responsibility and communicates with adjacent layers through well-defined interfaces.


## 2. UI Layer

* **Responsibilities**:

  * Render views using Avalonia.
  * Handle user input and bind data through ViewModels.
  * Translate user actions into service requests.
* **Composition**:

  * Views (XAML + code behind)
  * ViewModels
* **Interactions**:

  * Communicates only with the Service Layer.
  * Receives observable data updates from the Service Layer.
  * Does not access Repository or Domain directly.


## 3. Service Layer

* **Responsibilities**:

  * Orchestrate business operations.
  * Enforce business rules and validation.
  * Coordinate between UI and Repository.
* **Composition**:

  * Application services (classes implementing operations)
  * DTOs for transferring data between layers
* **Interactions**:

  * Receives requests from UI Layer.
  * Uses Repository Layer for data persistence.
  * Invokes Domain Layer for complex business logic.


## 4. Repository Layer

* **Responsibilities**:

  * Provide abstracted access to data sources (files, databases, web APIs).
  * Handle CRUD operations, caching, and persistence.
* **Composition**:

  * Interfaces for repositories
  * Concrete implementations for each storage type
* **Interactions**:

  * Called by Service Layer.
  * Does not know about UI or ViewModels.
  * Returns domain entities or DTOs.

## 5. Domain Layer

* **Responsibilities**:

  * Contain core business models and logic.
  * Ensure integrity and rules are enforced.
* **Composition**:

  * Domain entities
  * Value objects
  * Domain services (business logic without persistence)
* **Interactions**:

  * Invoked by Service Layer for computations or business rules.
  * Isolated from UI and Repository.

---

## 6. Component Interaction

* UI → Service → Repository → Domain
* Service Layer mediates all communication between UI and Repository/Domain.
* Each layer only depends on the layer directly below it.
* Communication via interfaces and DTOs to avoid tight coupling.
* Events or observable patterns used for asynchronous updates to UI.


**Notes**:

* Service Layer can adapt to different UI platforms (Desktop, WASM) without changing business rules.