import React, { useEffect, useRef } from 'react';
import 'ol/ol.css';
import { Map, View } from 'ol';
import { DragRotate, defaults as defaultInteractions } from 'ol/interaction';
import { getCenter } from 'ol/extent';
import { extent } from './constants';
import { createOSMLayer } from '../layers/OSMLayer';
import { createImageLayer } from '../layers/ImageLayerComponent';
import { createNodosLayer } from '../layers/NodosLayer';
import { createLineasLayer } from '../layers/LineasLayer';

const MapComponent: React.FC = () => {
    const mapRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const map = new Map({
            target: mapRef.current!,
            layers: [
                createOSMLayer(),
                createImageLayer(),
                createLineasLayer(),
                createNodosLayer(),
            ],
            view: new View({
                projection: 'EPSG:3857',
                center: getCenter(extent),
                zoom: 18.5,
                minZoom: 18.5,
                extent: extent,
                rotation: 46 * (Math.PI / 180),
            }),
            interactions: defaultInteractions().extend([new DragRotate()]),
        });

        map.on('singleclick', (evt) => {
            map.forEachFeatureAtPixel(evt.pixel, (feature) => {
                const props = feature.getProperties();
                alert(`Hiciste clic en el nodo: ${props.Nombre || 'sin nombre'}`);
                console.log('Feature clickeada:', props);
            });
        });

        return () => map.setTarget();
    }, []);

    return <div ref={mapRef} style={{ width: '100vw', height: '100vh' }} />;
};

export default MapComponent;
