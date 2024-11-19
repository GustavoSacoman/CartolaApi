import React from 'react';
import { Carousel } from 'primereact/carousel';
import './Carousel.css';

export default function CarouselComponent({ slides }) {
    const itemTemplate = (slide) => (
        <div className="carousel-slide">
            <img 
                src={slide.image} 
                alt={slide.title} 
                className="carousel-image"
            />
        </div>
    );

    return (
        <div className="carousel-wrapper">
            <Carousel 
                value={slides}
                numVisible={1}
                numScroll={1}
                itemTemplate={itemTemplate}
                circular
                autoplayInterval={5000}
                className="carousel-container"
            />
        </div>
    );
}
        