# Dev Companion Build Tests

This project contains tests that are meant to be run in a build pipeline after the project has been built. As well as asynchronously in local environments during development.

Test include a mix of Unit Tests and Internal Integration Tests.

## Definitions

### Unit Tests
Unit tests are tests that test functionality within a class and only that class.

Any external dependencies that a class interacts with in any manner must be passed in through the class's constructor and allow mocking, which the unit test will the mock all dependencies to pass into the constructor.

### Integration Tests
Integration tests are any test that can't be classified as a unit test.

This may be because the class is not setup in a way that allows mocking dependencies that are external to the class. Or because the test itself simply chooses not to mock a dependency, because part of the desire of the test is to verify integration with 1 or more external dependencies.

### Internal Integration Tests
Internal integration tests are tests that allow classes to interact with dependencies external to the class being tested, but not dependencies external to the application.

### External Integration Tests
External integration tests are tests that allow classes to interact with dependencies external to the application itself. A few examples include external HTTP endpoints, databases, or interactions with the OS's file system.