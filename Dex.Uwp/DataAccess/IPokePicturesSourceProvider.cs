﻿using Dex.Uwp.Services;
using System;
using System.Linq;
using System.Reflection;

namespace Dex.Uwp.DataAccess
{
    public interface IPokePicturesSourceProvider
    {
        IPokePicturesSource Source { get; set; }

        event ChangedEventHandler SourceChanged;

        IPokePicturesSource[] AvailableSources { get; }
    }

    public delegate void ChangedEventHandler();

    public class PokePicturesSourceProvider : IPokePicturesSourceProvider
    {
        private IPokePicturesSource _Source;
        private INavigationService _navigationService;

        public event ChangedEventHandler SourceChanged;

        public PokePicturesSourceProvider(INavigationService navigationService)
        {
            AvailableSources = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(type => typeof(IPokePicturesSource).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                .Select(type => Activator.CreateInstance(type))
                .Cast<IPokePicturesSource>()
                .ToArray();

            _Source = AvailableSources.Where(item => item is DefaultPokePicturesSource).First();
        }

        public IPokePicturesSource Source {
            get { return _Source; }

            set {
                _Source = value;
                SourceChanged?.Invoke();
            }
        }

        public IPokePicturesSource[] AvailableSources { get; }
    }

    
}
