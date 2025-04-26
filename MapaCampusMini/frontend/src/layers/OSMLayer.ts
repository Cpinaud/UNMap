import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';

export const createOSMLayer = () => {
    return new TileLayer({
        source: new OSM(),
    });
};
