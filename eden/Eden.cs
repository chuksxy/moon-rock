using System;
using System.Collections.Generic;
using System.Linq;

namespace eden {

      public static class Eden {

            private static readonly Dictionary<string, IService> RegisteredServices = new Dictionary<string, IService>();


            // GetTable Service by type.
            public static TService GetService<TService>() where TService : IService {
                  return (TService) RegisteredServices[typeof(TService).Name];
            }


            // IService is a marker interface for all eden micro services.
            public interface IService {

                  // ServiceUnit<S> Register();

            }

            public class ServiceUnit<S> {

                  private ServiceUnit(S service) {
                        Service     = service;
                        Initialized = true;
                  }


                  public static ServiceUnit<S> New() {
                        return new ServiceUnit<S>(default);
                  }


                  public ServiceUnit<S> Init(S service) {
                        if (Service != null) {
                              return this;
                        }

                        Initialized = true;
                        Service     = service;
                        return this;
                  }


                  private S Service { get; set; }

                  private bool Initialized { get; set; }

            }


            public class Configure {

                  private readonly HashSet<IService> _allServices = new HashSet<IService>();

                  private Configure() { }


                  // New Eden Config.
                  public static Configure New() {
                        return new Configure();
                  }


                  public Configure Register(IService service) {
                        _allServices.Add(service);
                        return this;
                  }


                  // Init all services registered.
                  public void Init() {
                        _allServices.ToList().ForEach(service => RegisteredServices.Add(service.GetType().Name, service));
                  }

            }

      }

}