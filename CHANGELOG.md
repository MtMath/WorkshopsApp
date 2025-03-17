# [1.2.0](https://github.com/MtMath/WorkshopsApp/compare/v1.1.0...v1.2.0) (2025-03-17)


### Features

* **Migration:** Add Db Contexts Initializer to migrations ([d929e2e](https://github.com/MtMath/WorkshopsApp/commit/d929e2ece7e666bfad1b7e6087e4681f0a5f26b9))
* **Migration:** Add injections for migrate ([468aa91](https://github.com/MtMath/WorkshopsApp/commit/468aa9158c02e5d915f3a550e1a0fb60f4721103))

# [1.1.0](https://github.com/MtMath/WorkshopsApp/compare/v1.0.0...v1.1.0) (2025-03-17)


### Bug Fixes

* Change Slug to Attedend property ([af86d1e](https://github.com/MtMath/WorkshopsApp/commit/af86d1ecce75fa3bcaf5170ae4bb8a74b55bdbfb))
* Remove sealed from Entities ([a641557](https://github.com/MtMath/WorkshopsApp/commit/a64155790ad13d18c796e818ceeb3617b6251fa2))
* Return entity in Put operations ([b3f432a](https://github.com/MtMath/WorkshopsApp/commit/b3f432ac2f48071df8323da34841d57ef491dafc))
* Update relationship ids ([77f8616](https://github.com/MtMath/WorkshopsApp/commit/77f8616ee2330736f4a0c340bac0352d69a374b5))


### Features

* Add Attendees Consult Endpoint ([bbe1a8a](https://github.com/MtMath/WorkshopsApp/commit/bbe1a8ab575e3dbebf1f4bf7dc351479fa685194))
* **Controller:** Add AttendeesRecord Controller ([59e2a76](https://github.com/MtMath/WorkshopsApp/commit/59e2a760d610678b7bd698e446d3d3ee9f388652))
* **Migration:** Add AttendeesRecord index migration ([1d7c5ab](https://github.com/MtMath/WorkshopsApp/commit/1d7c5abc7b74a95ec07d3f72db4789a9eda58712))
* Add AttendeesRecords Service ([0985e8b](https://github.com/MtMath/WorkshopsApp/commit/0985e8be61096e4c7255f386258cb91da50045f5))
* **Controller:** Add Collaborator Controller ([1d6be60](https://github.com/MtMath/WorkshopsApp/commit/1d6be60689a7cd98fba58468388dfb3218c6f4e4))
* **Controller:** Add Collaborator Controller ([fe5ad23](https://github.com/MtMath/WorkshopsApp/commit/fe5ad23dba1e4ff7b83550810c4676cd74da160f))


### Performance Improvements

* Add way to filter data by repository ([b607d55](https://github.com/MtMath/WorkshopsApp/commit/b607d55a77e262eaf3232938b73c1ee9df706cee))

# 1.0.0 (2025-03-16)


### Bug Fixes

* Adjust Properties Values generations ([2f90fb8](https://github.com/MtMath/WorkshopsApp/commit/2f90fb8525e967c225392eee3a6704c2b5e9f598))
* Miss drawSQL reference image ([793bffb](https://github.com/MtMath/WorkshopsApp/commit/793bffb852203cd964cc5d736da25cedd4d46624))
* Organize project structure ([458f660](https://github.com/MtMath/WorkshopsApp/commit/458f660e4a03c6f45357d950589bc42e706770e0))
* remove infra declaratives ([09219ff](https://github.com/MtMath/WorkshopsApp/commit/09219ff9c369375140570c1e9b1de8b6abffc2b8))
* Wrong slug verification ([a18a279](https://github.com/MtMath/WorkshopsApp/commit/a18a27975f466d3c9ebcec75362d97bb6e1f9ab9))
* **Identity:** Adjusts on initial setup ([2547d69](https://github.com/MtMath/WorkshopsApp/commit/2547d69b896b7867412ddd456a53bb5e65d1b745))
* **Infra:** Remove redundant injections ([3395475](https://github.com/MtMath/WorkshopsApp/commit/3395475df4239369f36b2731a69dc1e8480cb240))


### Features

* **Applicatio:** Add simple CollaboratorService ([0aaf2a5](https://github.com/MtMath/WorkshopsApp/commit/0aaf2a538a124b41b16e520c0c1f615ff326d58c))
* **Application:** Add DI for application layer ([6ea0d7a](https://github.com/MtMath/WorkshopsApp/commit/6ea0d7a79c33440a5f1432447eeb97f9da58ec1b))
* **Application:** Add FutureDate attribute validation ([d2be41b](https://github.com/MtMath/WorkshopsApp/commit/d2be41b530ea4782031f1057306295e912355ed2))
* **Application:** Add PaginetedList model ([8726e62](https://github.com/MtMath/WorkshopsApp/commit/8726e6205f0c679bf0dd09e6d6630004f55af87f))
* **Application:** Add simple CollaboratorService ([0571817](https://github.com/MtMath/WorkshopsApp/commit/057181703a2b9032e11f5b69d3ba1a5018ef1ebe))
* **Application:** Add WorkshopRequest dto ([01f5366](https://github.com/MtMath/WorkshopsApp/commit/01f5366b14166e225865db2366bfdc7a7fbb99b2))
* **Controller:** Add scaffolding controller ([ca8564b](https://github.com/MtMath/WorkshopsApp/commit/ca8564b813fa829730c1d39348b8e91c9dc8e465))
* **Controller:** Add Workshops Api Controller ([33fe871](https://github.com/MtMath/WorkshopsApp/commit/33fe87119c66c4a04724abfa0699f33f77a0df8a))
* **Domain:** Add Attendees entity ([35277e2](https://github.com/MtMath/WorkshopsApp/commit/35277e21500a303e9eeba561f5e9b39e4148f844))
* **Domain:** Add Base Entity class ([90c849a](https://github.com/MtMath/WorkshopsApp/commit/90c849a35f1e3b892317288815360ea978d0093e))
* **Domain:** Add Collaborator entity ([36d8eef](https://github.com/MtMath/WorkshopsApp/commit/36d8eef12afbf809a415466510a086d4eee5abfe))
* **Domain:** Add Generic Repository contract ([07fcf0e](https://github.com/MtMath/WorkshopsApp/commit/07fcf0e2da4938eb426eb9550a3fb276001a6d0e))
* **Domain:** Add ICollection navigation for entities ([48412c6](https://github.com/MtMath/WorkshopsApp/commit/48412c69e66be2a8e62cd12d8744621bf6cb7448))
* **Domain:** Add Workshops entity ([5b75a5e](https://github.com/MtMath/WorkshopsApp/commit/5b75a5e00069481e3206d787411d07d055ca04a1))
* **DTO:** Add CollaboratorRequestDto ([bf4f9dc](https://github.com/MtMath/WorkshopsApp/commit/bf4f9dc2437308f6dec43a1fc83deaec978e109b))
* **DTO:** Add Generic response dto ([e0d2b86](https://github.com/MtMath/WorkshopsApp/commit/e0d2b862195e7bca36f3156dc79bd083ccb530c7))
* **Identity:** Create Identity User ([fd0cfe1](https://github.com/MtMath/WorkshopsApp/commit/fd0cfe190b9daacf755c4585d1aa7c07d926ce86))
* **Migration:** Add Auditable Properties values ([c555867](https://github.com/MtMath/WorkshopsApp/commit/c55586729bd3b6969cbd85868052e4b83d75f66c))
* Add Layers to injection container ([e6e86cc](https://github.com/MtMath/WorkshopsApp/commit/e6e86cc0fccf269608325f1ddea954b45cec1a26))
* **DTO:** Add simple AttendeeRequestDto ([8bf890a](https://github.com/MtMath/WorkshopsApp/commit/8bf890a8c27e6796ae86a6445b028eb8e0895ba6))
* **Infra:** Add Generic Repository concrete ([a5d8545](https://github.com/MtMath/WorkshopsApp/commit/a5d854583761a807b0ad368b80be040cb79256e5))
* **Migration:** Add Initial Entities migration ([ba53c35](https://github.com/MtMath/WorkshopsApp/commit/ba53c3506b6014e661b6944d849bc3a77f061815))
* **Services:** Add simple WorkshopsServices ([3079838](https://github.com/MtMath/WorkshopsApp/commit/3079838a7b0f12d833c776ce19f836ebe8e49e54))
* **Utils:** Add Slug helper for titles uniquess ([1cd4229](https://github.com/MtMath/WorkshopsApp/commit/1cd4229d44fd7cd150fe1931f4ae70216026b57d))
* Add App DbContext for domain entities ([46e4633](https://github.com/MtMath/WorkshopsApp/commit/46e46333b5644b32afd0e5a7624af981863c59d0))
* Add CrossCutting for services extensions ([4ebcf46](https://github.com/MtMath/WorkshopsApp/commit/4ebcf462b036fb7a9e19137f165dffd32a9107f1))
* Add Exception Handler to application ([72bdfa2](https://github.com/MtMath/WorkshopsApp/commit/72bdfa23ca409de49da748d830ae6ef9f62e22fe))
* Add Identity Injection ([779b959](https://github.com/MtMath/WorkshopsApp/commit/779b9599bf0f99d9c80cc59d26c93493fe8007ae))
* Add Roles in domain layer ([10f28cd](https://github.com/MtMath/WorkshopsApp/commit/10f28cd9bd168c37c6d59da70936983062609844))
* Add swagger Authorization and Versioning ([97c4783](https://github.com/MtMath/WorkshopsApp/commit/97c478389d858c91b0ec52f557a9b8831f263117))
* Add to infra AppDbContext ([14609bf](https://github.com/MtMath/WorkshopsApp/commit/14609bf6dd00f99b543c84aeaa61476ed664bc9e))
* Map Identity with right version set ([cbe9c30](https://github.com/MtMath/WorkshopsApp/commit/cbe9c300c13523c0305ce2795cdc81eead521f28))
* **Identity:** Implement Identity DbContext ([85d1433](https://github.com/MtMath/WorkshopsApp/commit/85d14331a8c26bea786f1af2af764b18e5492c66))


### Performance Improvements

* **Services:** Return entity in Put operations ([e42d2df](https://github.com/MtMath/WorkshopsApp/commit/e42d2df932b8d51f7c78029d1382c2774a053f8f))
