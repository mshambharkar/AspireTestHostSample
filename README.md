# MultiProj

## Structure
- WebAppA : Web App A with endpoint /api/webappa
- WebAppB : Web App B with endpoint /api/webappb calling /api/webappa
- IntTestWebHostFactory : Integration test using WebHostFactory
- AspireAppHost : App host for both web apps, it will take care of updating service address during run
- AspireHostTest: App host test similar to integration test, all web apps are now running as single unit, during test service discovery is updated with http address of webappa
- 
